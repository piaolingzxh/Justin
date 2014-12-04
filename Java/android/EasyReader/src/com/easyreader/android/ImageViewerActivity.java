package com.easyreader.android;

import java.io.FileInputStream;
import java.io.FileNotFoundException;

import com.easyreader.android.R;
import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.graphics.PointF;
import android.os.Bundle;
import android.util.FloatMath;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.ImageView;
import android.widget.Toast;

public class ImageViewerActivity extends Activity implements OnTouchListener {
	private static final String TAG = "Touch";
	// These matrices will be used to move and zoom image
	Matrix matrix = new Matrix();
	Matrix savedMatrix = new Matrix();
	ImageView image;
	// We can be in one of these 3 states
	static final int NONE = 0;
	static final int DRAG = 1;
	static final int ZOOM = 2;
	int mode = NONE;
	public static final int OPEN = Menu.FIRST;
	public static final int RETURN_MENU = Menu.FIRST + 1;
	public static final int QUIT = Menu.FIRST + 2;
	// Remember some things for zooming
	PointF start = new PointF();
	PointF mid = new PointF();
	float oldDist = 1f;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_imageview);
		image = (ImageView) findViewById(R.id.imageView);
		image.setOnTouchListener(this);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		menu.add(0, OPEN, 0, "OPEN");
		menu.add(0, RETURN_MENU, 1, "杩斿洖涓昏彍鍗�");
		menu.add(0, QUIT, 2, "閫�鍑�");
		return super.onCreateOptionsMenu(menu);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		super.onOptionsItemSelected(item);
		switch (item.getItemId()) {
		case OPEN:
			Toast.makeText(ImageViewerActivity.this, "淇濆瓨鎸夐挳琚偣鍑�!",
					Toast.LENGTH_LONG).show();
			try {
				Intent browserIntent = new Intent(ImageViewerActivity.this,
						FileBrowserActivity.class);
				Bundle bundle = new Bundle();
				bundle.putString("fileName", getTitle().toString());
				browserIntent.putExtra("bd", bundle);
				startActivityForResult(browserIntent, 10);
			} catch (Exception e) {
				Toast.makeText(ImageViewerActivity.this, e.toString(),
						Toast.LENGTH_LONG).show();
				Log.e("toBrowser", e.toString());
			}

			break;
		case RETURN_MENU:
			Toast.makeText(ImageViewerActivity.this, "杩斿洖涓昏彍鍗曟寜閽鐐瑰嚮!",
					Toast.LENGTH_LONG).show();

			break;
		case QUIT:
			Toast.makeText(ImageViewerActivity.this, "閫�鍑烘寜閽鐐瑰嚮!",
					Toast.LENGTH_LONG).show();
			break;
		}
		return false;
	}

	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		Toast.makeText(ImageViewerActivity.this, "onActivityResult",
				Toast.LENGTH_LONG).show();
		try {
			if (requestCode == 10) {
				if (resultCode == RESULT_CANCELED) {
					setTitle("Canceled");
				} else if (RESULT_OK == resultCode) {
					String temp = "";
					Bundle bundle = data.getExtras();
					if (bundle != null) {
						temp = bundle.getString("fileName");
					}
					setTitle(temp);
					Bitmap bitmap = getLoacalBitmap(temp); // 浠庢湰鍦板彇鍥剧墖
					// Bitmap bitmap =
					// getHttpBitmap("http://blog.3gstdy.com/wp-content/themes/twentyten/images/headers/path.jpg");
					// 浠庣綉涓婂彇鍥剧墖
					image.setImageBitmap(bitmap); // 璁剧疆Bitmap
				}
			}
		} catch (Exception ex) {
			Toast.makeText(ImageViewerActivity.this, ex.toString(),
					Toast.LENGTH_LONG).show();
		}
	}

	public static Bitmap getLoacalBitmap(String url) {
		try {
			FileInputStream fis = new FileInputStream(url);
			return BitmapFactory.decodeStream(fis);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
			return null;
		}
	}

	@Override
	public boolean onTouch(View v, MotionEvent event) {
		ImageView view = (ImageView) v;

		// Dump touch event to log
		dumpEvent(event);

		// Handle touch events here...
		switch (event.getAction() & MotionEvent.ACTION_MASK) {
		case MotionEvent.ACTION_DOWN:
			savedMatrix.set(matrix);
			start.set(event.getX(), event.getY());
			Log.d(TAG, "mode=DRAG");
			mode = DRAG;
			break;
		case MotionEvent.ACTION_POINTER_DOWN:
			oldDist = spacing(event);
			Log.d(TAG, "oldDist=" + oldDist);
			if (oldDist > 10f) {
				savedMatrix.set(matrix);
				midPoint(mid, event);
				mode = ZOOM;
				Log.d(TAG, "mode=ZOOM");
			}
			break;
		case MotionEvent.ACTION_UP:
		case MotionEvent.ACTION_POINTER_UP:
			mode = NONE;
			Log.d(TAG, "mode=NONE");
			break;
		case MotionEvent.ACTION_MOVE:
			if (mode == DRAG) {
				// ...
				matrix.set(savedMatrix);
				matrix.postTranslate(event.getX() - start.x, event.getY()
						- start.y);
			} else if (mode == ZOOM) {
				float newDist = spacing(event);
				Log.d(TAG, "newDist=" + newDist);
				if (newDist > 10f) {
					matrix.set(savedMatrix);
					float scale = newDist / oldDist;
					matrix.postScale(scale, scale, mid.x, mid.y);
				}
			}
			break;
		}

		view.setImageMatrix(matrix);
		return true; // indicate event was handled
	}

	/** Show an event in the LogCat view, for debugging */
	private void dumpEvent(MotionEvent event) {
		String names[] = { "DOWN", "UP", "MOVE", "CANCEL", "OUTSIDE",
				"POINTER_DOWN", "POINTER_UP", "7?", "8?", "9?" };
		StringBuilder sb = new StringBuilder();
		int action = event.getAction();
		int actionCode = action & MotionEvent.ACTION_MASK;
		sb.append("event ACTION_").append(names[actionCode]);
		if (actionCode == MotionEvent.ACTION_POINTER_DOWN
				|| actionCode == MotionEvent.ACTION_POINTER_UP) {
			sb.append("(pid ").append(
					action >> MotionEvent.ACTION_POINTER_ID_SHIFT);
			sb.append(")");
		}
		sb.append("[");
		for (int i = 0; i < event.getPointerCount(); i++) {
			sb.append("#").append(i);
			sb.append("(pid ").append(event.getPointerId(i));
			sb.append(")=").append((int) event.getX(i));
			sb.append(",").append((int) event.getY(i));
			if (i + 1 < event.getPointerCount())
				sb.append(";");
		}
		sb.append("]");
		Log.d(TAG, sb.toString());
	}

	/** Determine the space between the first two fingers */
	private float spacing(MotionEvent event) {
		float x = event.getX(0) - event.getX(1);
		float y = event.getY(0) - event.getY(1);
		return FloatMath.sqrt(x * x + y * y);
	}

	/** Calculate the mid point of the first two fingers */
	private void midPoint(PointF point, MotionEvent event) {
		float x = event.getX(0) + event.getX(1);
		float y = event.getY(0) + event.getY(1);
		point.set(x / 2, y / 2);
	}

}
