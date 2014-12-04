package com.easyreader.android;

import com.easyreader.android.R;
import com.easyreader.android.FileBrowser;
import com.easyreader.android.OnFileBrowserListener;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;

public class FileBrowserActivity extends Activity implements
		OnFileBrowserListener {
	FileBrowser fileBrowser;

	@Override
	public void onFileItemClick(String filename) {
		setTitle(filename);
		Intent intent = new Intent(FileBrowserActivity.this,
				ImageViewerActivity.class);
		intent.putExtra("fileName", filename);
		setResult(RESULT_OK, intent); // intent涓篈浼犳潵鐨勫甫鏈塀undle鐨刬ntent锛屽綋鐒朵篃鍙互鑷繁瀹氫箟鏂扮殑Bundle
		finish();// 姝ゅ涓�瀹氳璋冪敤finish()鏂规硶
	}

	@Override
	public void onDirItemClick(String path) {
		setTitle(path);
		
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub		
		super.onCreate(savedInstanceState);		
		setContentView(R.layout.activity_file_browser);
		String fileName=getIntent().getBundleExtra("bd").getString("fileName");
		fileBrowser = (FileBrowser) findViewById(R.id.filebrowser);
		fileBrowser.setOnFileBrowserListener(this);
		fileBrowser.brower(fileName);
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_BACK && event.getRepeatCount() == 0) {
			if (!fileBrowser.getCanUp())
				finish();
			fileBrowser.Up();
			return true;
		} else
			return super.onKeyDown(keyCode, event);
	}
}