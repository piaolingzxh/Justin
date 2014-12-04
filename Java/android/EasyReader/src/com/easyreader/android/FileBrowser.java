package com.easyreader.android;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Stack;
import android.content.Context;
import android.util.AttributeSet;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

public class FileBrowser extends ListView implements
		android.widget.AdapterView.OnItemClickListener {
	private final String namespace = "http://mobile.blogjava.net";

	private List<File> fileList = new ArrayList<File>();
	private Stack<String> dirStack = new Stack<String>();
	private FileListAdapter fileListAdapter;
	private OnFileBrowserListener onFileBrowserListener;
	private int folderImageResId;
	private int otherFileImageResId;
	private Map<String, Integer> fileImageResIdMap = new HashMap<String, Integer>();
	private boolean onlyFolder = false;
	public String CurrentDir = "";

	public FileBrowser(Context context, AttributeSet attrs) {
		super(context, attrs);

		// android.os.Environment.getExternalStorageDirectory()
		// .toString();
		setOnItemClickListener(this);
		setBackgroundColor(android.graphics.Color.WHITE);

		folderImageResId = attrs.getAttributeResourceValue(namespace,
				"folderImage", 0);
		otherFileImageResId = attrs.getAttributeResourceValue(namespace,
				"otherFileImage", 0);
		onlyFolder = attrs.getAttributeBooleanValue(namespace, "onlyFolder",
				false);
		int index = 1;
		while (true) {
			String extName = attrs.getAttributeValue(namespace, "extName"
					+ index);
			int fileImageResId = attrs.getAttributeResourceValue(namespace,
					"fileImage" + index, 0);

			if ("".equals(extName) || extName == null || fileImageResId == 0) {
				break;
			}
			fileImageResIdMap.put(extName, fileImageResId);
			index++;
		}
		fileListAdapter = new FileListAdapter(getContext());
		setAdapter(fileListAdapter);
		/*brower("/");*/
	}

	public void brower(String dir) {
		if (dir == null)
			dir = "/";
		File file = new File(dir);
		if (file.isFile()) {
			dir = file.getParent();
		}
		File dirFile = new File(dir);
		if (!dirFile.exists()) {
			dir = "/";
		}
		dirStack.clear();
		dirStack.push(dir);
		addFiles(dir);
	}

	private void addFiles(String dir) {
		CurrentDir = dir;
		fileList.clear();

		File[] files = new File(CurrentDir).listFiles();
		if (dirStack.size() > 1)
			fileList.add(null);
		for (File file : files) {
			if (onlyFolder) {
				if (file.isDirectory())
					fileList.add(file);
			} else {
				fileList.add(file);
			}
		}
	}

	private String getCurrentPath() {
		String path = "";
		for (String dir : dirStack) {
			path += dir + "/";
		}
		path = path.substring(0, path.length() - 1);
		return path;
	}

	private String getExtName(String filename) {

		int position = filename.lastIndexOf(".");
		if (position >= 0)
			return filename.substring(position + 1);
		else
			return "";
	}

	@Override
	public void onItemClick(AdapterView<?> parent, View view, int position,
			long id) {
		if (fileList.get(position) == null) {
			Up();
		} else if (fileList.get(position).isDirectory()) {
			try {
				dirStack.push(fileList.get(position).getName());

				addFiles(getCurrentPath());
				fileListAdapter.notifyDataSetChanged();
				if (onFileBrowserListener != null) {
					onFileBrowserListener.onDirItemClick(getCurrentPath());
				}
			} catch (Exception ex) {
			}
		} else {
			try {
				if (onFileBrowserListener != null) {
					String filename = getCurrentPath() + "/"
							+ fileList.get(position).getName();
					onFileBrowserListener.onFileItemClick(filename);
				}
			} catch (Exception ex) {
			}
		}

	}

	private class FileListAdapter extends BaseAdapter {
		private Context context;

		public FileListAdapter(Context context) {
			this.context = context;
		}

		@Override
		public int getCount() {
			return fileList.size();
		}

		@Override
		public Object getItem(int position) {
			return fileList.get(position);
		}

		@Override
		public long getItemId(int position) {
			return position;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			LinearLayout fileLayout = new LinearLayout(context);
			fileLayout.setLayoutParams(new LayoutParams(
					LayoutParams.FILL_PARENT, LayoutParams.WRAP_CONTENT));
			fileLayout.setOrientation(LinearLayout.HORIZONTAL);
			fileLayout.setPadding(5, 10, 0, 10);
			ImageView ivFile = new ImageView(context);
			ivFile.setLayoutParams(new LayoutParams(48, 48));
			TextView tvFile = new TextView(context);
			tvFile.setTextColor(android.graphics.Color.RED);
			tvFile.setTextAppearance(context,
					android.R.style.TextAppearance_Medium);

			tvFile.setPadding(5, 5, 0, 0);
			if (fileList.get(position) == null) {

				if (folderImageResId > 0)
					ivFile.setImageResource(folderImageResId);
				tvFile.setText(". .");
			} else if (fileList.get(position).isDirectory()) {
				if (folderImageResId > 0)
					ivFile.setImageResource(folderImageResId);
				tvFile.setText(fileList.get(position).getName());
			} else {
				tvFile.setText(fileList.get(position).getName());
				Integer resId = fileImageResIdMap.get(getExtName(fileList.get(
						position).getName()));
				int fileImageResId = 0;
				if (resId != null) {
					if (resId > 0) {
						fileImageResId = resId;
					}

				}
				if (fileImageResId > 0)
					ivFile.setImageResource(fileImageResId);
				else if (otherFileImageResId > 0)
					ivFile.setImageResource(otherFileImageResId);
			}

			tvFile.setLayoutParams(new LayoutParams(LayoutParams.FILL_PARENT,
					LayoutParams.WRAP_CONTENT));
			fileLayout.addView(ivFile);
			fileLayout.addView(tvFile);
			return fileLayout;
		}
	}

	public boolean getCanUp() {
		return dirStack.size() > 1;
	}

	public void Up() {
		try {
			dirStack.pop();
			addFiles(getCurrentPath());
			fileListAdapter.notifyDataSetChanged();
			if (onFileBrowserListener != null) {
				onFileBrowserListener.onDirItemClick(getCurrentPath());
			}
		} catch (Exception ex) {
		}
	}

	public void setOnFileBrowserListener(OnFileBrowserListener listener) {
		this.onFileBrowserListener = listener;
	}
}