package com.justin.reader.util;
 
import android.app.Application;

public class ReaderApp extends Application {
	
    private static ReaderApp instance;

    public static ReaderApp getInstance() {
        return instance;
    }

    @Override
    public void onCreate() {
        // TODO Auto-generated method stub
        super.onCreate();
        instance = this;
    }
}