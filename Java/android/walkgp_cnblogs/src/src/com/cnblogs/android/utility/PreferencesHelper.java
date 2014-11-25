package com.cnblogs.android.utility;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Resources;

import com.cnblogs.android.R;

public class PreferencesHelper {

	public static Context MyContext=GlobalAppcation.getInstance();
	public static Resources Res=MyContext.getResources();
	
	public static SharedPreferences getPreferences()
	{
		return  MyContext.getSharedPreferences(
				Res.getString(R.string.preferences_key), MyContext.MODE_PRIVATE);
	}
	public static String getStroedTabName(String defaultTabName) {
		 
		SharedPreferences settings =getPreferences();
		String sotredTabName = settings.getString(
				Res.getString(R.string.preferences_current_tab), defaultTabName);
		return sotredTabName;
	}
	
	public static void setStoredTabName(String tabName)
	{
		SharedPreferences settings = getPreferences();
		SharedPreferences.Editor editor = settings.edit();
		editor.putString(Res.getString(R.string.preferences_current_tab),
				tabName);
		editor.commit();
	}

}
