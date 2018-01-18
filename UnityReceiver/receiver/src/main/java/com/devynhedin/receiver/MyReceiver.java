package com.devynhedin.receiver;

import android.content.BroadcastReceiver;
import android.content.Intent;
import android.content.Context;
import android.util.Log;

public class MyReceiver extends BroadcastReceiver {

    private static MyReceiver instance;

    //Delta values to be read by Unity
    public static String delta = "";

    //Triggered when an Intent is caught
    @Override
    public void onReceive(Context context, Intent intent) {
        //Extract data from Intent
        String receivedIntent = intent.getStringExtra(Intent.EXTRA_TEXT);
        Log.d("IntentReceived", "Intent has been successfully received.");
        if (receivedIntent != null) {
            // We assigned it to our static variable
            delta = receivedIntent;
        }
    }

    //Creates receiver instance
    public static void createInstance()
    {
        if(instance ==  null)
        {
            Log.d("status", "Instance is created successfully.");
            instance = new MyReceiver();
        }

    }
}