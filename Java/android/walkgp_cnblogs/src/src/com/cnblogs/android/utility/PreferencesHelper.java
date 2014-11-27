package com.cnblogs.android.utility;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.webkit.WebSettings.TextSize;

import com.cnblogs.android.R;
import com.cnblogs.android.SettingActivity;

public class PreferencesHelper {

	public static Context MyContext = GlobalAppcation.getInstance();
	public static Resources Res = MyContext.getResources();

	public static SharedPreferences getPreferences() {
		return MyContext
				.getSharedPreferences(Res.getString(R.string.preferences_key),
						MyContext.MODE_PRIVATE);
	}

	public static String getStroedTabName(String defaultTabName) {

		SharedPreferences settings = getPreferences();
		String sotredTabName = settings
				.getString(Res.getString(R.string.preferences_current_tab),
						defaultTabName);
		return sotredTabName;
	}

	public static void setStoredTabName(String tabName) {
		SharedPreferences settings = getPreferences();
		SharedPreferences.Editor editor = settings.edit();
		editor.putString(Res.getString(R.string.preferences_current_tab),
				tabName);
		editor.commit();
	}

	public static TextSize getStoredTextSize() {
		String fontSize = getPreferences().getString(
				SettingActivity.CONFIG_FONT_SIZE_OPTION_KEY, "SMALLEST");
		return Enum.valueOf(TextSize.class, fontSize.toString());
	}

	public static boolean getIsAutoHorizontal() {
		return getPreferences().getBoolean(
				SettingActivity.CONFIG_IS_HORIZONTAL, true);
	}

	public static boolean getAllowFullScreen() {
		return getPreferences().getBoolean(
				Res.getString(R.string.preferences_is_fullscreen), false);
	}

	public static boolean getIsLoadImageAuto() {
		String readMode = getPreferences().getString(
				SettingActivity.CONFIG_READ_MODE_OPTION_KEY, "0");

		return readMode.equalsIgnoreCase("0");
	}

}
