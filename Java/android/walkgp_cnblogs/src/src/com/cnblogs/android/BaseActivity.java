package com.cnblogs.android;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.view.KeyEvent;
import android.webkit.WebSettings.TextSize;

/**
 * 基类，大部分Activity继承自此类
 * 
 * @author walkingp
 * @date:2011-11
 * 
 */
public class BaseActivity extends Activity {

	SharedPreferences settings;
	TextSize textSize =TextSize.SMALLEST;

	@Override
	protected void onResume() {
		super.onResume();

		settings = getSharedPreferences(getString(R.string.preferences_key), 0);

		String fontSize = settings.getString(
				SettingActivity.CONFIG_FONT_SIZE_OPTION_KEY, "SMALLEST");
		textSize = Enum.valueOf(TextSize.class, fontSize.toString());
		
		if (!SettingActivity.getIsAutoHorizontal(this))
			setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		else
			setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_USER);
	}

	protected void onPause() {
		super.onPause();
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	}

	/**
	 * 按下键盘上返回按钮
	 */
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_SEARCH) {// 搜索
			Intent intent = new Intent(BaseActivity.this, SearchActivity.class);
			intent.putExtra("isShowQuitHints", false);
			startActivity(intent);
			return true;
		} else {
			return super.onKeyDown(keyCode, event);
		}
	}
}
