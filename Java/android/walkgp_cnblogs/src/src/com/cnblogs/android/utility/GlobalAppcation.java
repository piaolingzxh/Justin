package com.cnblogs.android.utility;
import android.app.Application;

public class GlobalAppcation extends Application {
	
    private static GlobalAppcation instance;

    public static GlobalAppcation getInstance() {
        return instance;
    }

    @Override
    public void onCreate() {
        // TODO Auto-generated method stub
        super.onCreate();
        instance = this;
    }
}

 