package com.easyreader.android;

import com.easyreader.android.R;
import com.easyreader.android.FileBrowser;
import com.easyreader.android.OnFileBrowserListener;
import com.easyreader.android.util;

import android.app.Activity;
import android.os.Bundle;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import android.view.GestureDetector.OnGestureListener;
import android.view.View.OnTouchListener;
import android.webkit.WebView;
import android.widget.ImageView;
import android.widget.TextView;

public class MainActivity extends Activity implements OnFileBrowserListener,
		OnGestureListener {
	WebView webView;
	private boolean showLines = true;
	private boolean wordwrap = true;
	boolean allowFullScreen = true;
	String fileName = "";
	private GestureDetector gestureScanner;// 手势

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		fileBrowser = (FileBrowser) findViewById(R.id.filebrowser);
		fileBrowser.setOnFileBrowserListener(this);

		webView = (WebView) findViewById(R.id.webView1);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.requestFocus();
		 
		webView.setOnTouchListener(new OnTouchListener() {
			@Override
			public boolean onTouch(View v, MotionEvent event) {
				return gestureScanner.onTouchEvent(event);
			}
		});
		// 监听屏幕动作事件 全屏
		gestureScanner = new GestureDetector(this);
		gestureScanner.setIsLongpressEnabled(true);
		gestureScanner
				.setOnDoubleTapListener(new GestureDetector.OnDoubleTapListener() {
					public boolean onDoubleTap(MotionEvent e) {
						
						if (fileBrowser.getVisibility() == View.VISIBLE) {
							fileBrowser.setVisibility(View.GONE);
							setFullScreen();
							 
						} else {
							quitFullScreen();
							fileBrowser.setVisibility(View.VISIBLE);
							 
						}						 
						return false;
					}

					public boolean onDoubleTapEvent(MotionEvent e) {
						return false;
					}

					public boolean onSingleTapConfirmed(MotionEvent e) {
						return false;
					}
				});
		fileBrowser.brower(fileName);
		util.fillWebView(webView, fileName, showLines, wordwrap);
	}

	FileBrowser fileBrowser;

	private void setFullScreen() {
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);
		// 隐藏导航
		fileBrowser.setVisibility(View.GONE);
	}

	/*
	 退出全屏
	 */
	private void quitFullScreen() {
		final WindowManager.LayoutParams attrs = getWindow().getAttributes();
		attrs.flags &= (~WindowManager.LayoutParams.FLAG_FULLSCREEN);
		getWindow().setAttributes(attrs);
		getWindow()
				.clearFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
		// 显示导航
		fileBrowser.setVisibility(View.VISIBLE);
	}

	@Override
	public void onFileItemClick(String filename) {

		/*setTitle(filename);
		Intent intent = new Intent(FileBrowserActivity.this,
				ImageViewerActivity.class);
		intent.putExtra("fileName", filename);
		setResult(RESULT_OK, intent); // intent为A传来的带有Bundle的intent，当然也可以自己定义新的Bundle
		finish();// 此处一定要调用finish()方法
		*/

		util.fillWebView(webView, filename, showLines, wordwrap);
		setTitle(fileName);
	}

	@Override
	public void onDirItemClick(String path) {
		/*setTitle(path);*/
		setTitle(path);
	}

	@Override
	public boolean onDown(MotionEvent e) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void onShowPress(MotionEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public boolean onSingleTapUp(MotionEvent e) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean onScroll(MotionEvent e1, MotionEvent e2, float distanceX,
			float distanceY) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void onLongPress(MotionEvent e) {
		// TODO Auto-generated method stub

	}

	@Override
	public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,
			float velocityY) {
		// TODO Auto-generated method stub
		return false;
	}

}