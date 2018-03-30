package com.devynhedin.bluetoothapp;

        import android.support.v7.app.AppCompatActivity;
        import android.os.Bundle;
        import android.os.Handler;
        import android.content.Intent;
        import android.util.Log;

        import android.app.Activity;
        import android.bluetooth.BluetoothAdapter;
        import android.bluetooth.BluetoothDevice;
        import android.bluetooth.BluetoothSocket;
        import android.content.Intent;
        import android.os.Bundle;
        import android.os.Handler;
        import android.util.Log;
        import android.view.View;
        import android.widget.Button;
        import android.widget.EditText;
        import android.widget.TextView;

        import java.io.IOException;
        import java.io.InputStream;
        import java.io.OutputStream;
        import java.util.Set;
        import java.util.UUID;

public class BTApp {

    TextView btOutput;
    EditText myTextbox;
    BluetoothAdapter mBluetoothAdapter;
    BluetoothSocket mmSocket;
    BluetoothDevice mmDevice;
    OutputStream mmOutputStream;
    InputStream mmInputStream;
    Thread workerThread;
    byte[] readBuffer;
    int readBufferPosition;
    int counter;
    volatile boolean stopWorker;

    public static BTApp instance = null;

    public static void createInstance()
    {
        if(instance ==  null)
        {
            Log.d("status", "Instance is created successfully.");
            instance = new BTApp();
        }

    }

//    @Override
//    protected void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        setContentView(R.layout.activity_main);
//
//        try {
//            findBT();
//            Log.d("BT", "onCreate: SUCCESS ON FINDING BT");
//            openBT();
//            Log.d("BT", "onCreate: SUCCESS ON OPENING BT");
//        } catch (IOException ex) { }
//
//
//    }
    public BTApp() {
        try {
            findBT();
            Log.d("BT", "onCreate: SUCCESS ON FINDING BT");
            openBT();
            Log.d("BT", "onCreate: SUCCESS ON OPENING BT");
        } catch (IOException ex) { }
    }
    void findBT() {
        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
        if(mBluetoothAdapter == null)
        {
            //btOutput.setText("No bluetooth adapter available");
            return;
        }

        if(!mBluetoothAdapter.isEnabled()) {
            Intent enableBluetooth = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
//            startActivityForResult(enableBluetooth, 0);
        }

        Set<BluetoothDevice> pairedDevices = mBluetoothAdapter.getBondedDevices();
        if(pairedDevices.size() > 0)
        {
            for(BluetoothDevice device : pairedDevices)
            {
                Log.d("bluetoothdevice",device.getName()+"");
                if(device.getName().equals("DIY-VIVE_CAPSTONE_BT"))
                {
                    mmDevice = device;
                    break;
                }
            }
        }
        //btOutput.setText("Bluetooth Device Found");
    }

    void openBT() throws IOException {
        if (mmDevice!=null) {
            UUID uuid = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"); //Standard SerialPortService ID
            mmSocket = mmDevice.createRfcommSocketToServiceRecord(uuid);
            mmSocket.connect();
            mmOutputStream = mmSocket.getOutputStream();
            mmInputStream = mmSocket.getInputStream();

            beginListenForData();

            //btOutput.setText("Bluetooth Opened");
        }else{

            //btOutput.setText("No Bluetooth Device");
        }
    }

    void beginListenForData() {
        final Handler handler = new Handler();
        final byte delimiter = 10; //This is the ASCII code for a newline character

        stopWorker = false;
        readBufferPosition = 0;
        readBuffer = new byte[1024];
        workerThread = new Thread(new Runnable() {
            public void run() {
                while (!Thread.currentThread().isInterrupted() && !stopWorker) {
                    try {
                        int bytesAvailable = mmInputStream.available();
                        if (bytesAvailable > 0) {
                            byte[] packetBytes = new byte[bytesAvailable];
                            mmInputStream.read(packetBytes);
                            for (int i = 0; i < bytesAvailable; i++) {
                                byte b = packetBytes[i];
                                if (b == delimiter) {
                                    byte[] encodedBytes = new byte[readBufferPosition];
                                    System.arraycopy(readBuffer, 0, encodedBytes, 0, encodedBytes.length);
                                    final String data = new String(encodedBytes, "US-ASCII");
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                    Intent sendIntent = new Intent();
//
//                                    // Flags that allow app to broadcast intent in the background
//                                    sendIntent.addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION | Intent.FLAG_FROM_BACKGROUND | Intent.FLAG_INCLUDE_STOPPED_PACKAGES);
//
//                                    //Identifies the sender of the broadcast
//                                    sendIntent.setAction("com.devynhedin.arapp.MainActivity");
//
//                                    //Populate the intent with a text string consisting of six floats
//                                    sendIntent.putExtra(Intent.EXTRA_TEXT, data);
//
//                                    //Sends the broadcast out for any receiver to pick it up
//                                    sendBroadcast(sendIntent);
//
//                                    //Logging coordinates to LogCat for debugging purposes
                                    Log.d("speedBroadcast", data);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                    readBufferPosition = 0;

                                    handler.post(new Runnable() {
                                        public void run() {
                                            //btOutput.setText(data);
                                        }
                                    });
                                } else {
                                    readBuffer[readBufferPosition++] = b;
                                }
                            }
                        }
                    } catch (IOException ex) {
                        stopWorker = true;
                    }
                }
            }
        });

        workerThread.start();
    }

    void sendData() throws IOException {
        String msg = myTextbox.getText().toString();
        msg += "\n";
        mmOutputStream.write(msg.getBytes());
        btOutput.setText("Data Sent");
    }

    void closeBT() throws IOException {
        stopWorker = true;
        mmOutputStream.close();
        mmInputStream.close();
        mmSocket.close();
        btOutput.setText("Bluetooth Closed");
    }
}
