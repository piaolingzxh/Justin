package org.anddev.android.filebrowser;

import java.io.File;
import java.net.URISyntaxException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ListActivity;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.DialogInterface.OnClickListener;
import android.graphics.drawable.Drawable;
import android.net.Uri;

import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.widget.Toast;

public class AndroidFileBrowser extends ListActivity {

	private enum DISPLAYMODE {
		ABSOLUTE, RELATIVE;
	}

	protected static final int SUB_ACTIVITY_REQUEST_CODE = 1337;

	private final DISPLAYMODE displayMode = DISPLAYMODE.RELATIVE;
	private List<IconifiedText> directoryEntries = new ArrayList<IconifiedText>();
	private File currentDirectory = new File("/");

	/** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle icicle) {
		super.onCreate(icicle);
		// setTheme(android.R.style.Theme_Dark);
		browseToRoot();
		this.setSelection(0);
	}

	// // ===========================================================
	// // Menu Creation & Reaction
	// // ===========================================================
	// @Override
	// public boolean onCreateOptionsMenu(Menu menu) {
	// menu.add(0,0, getString(R.string.menu_new_folder));
	// return super.onCreateOptionsMenu(menu);
	// }
	//
	// @Override
	// public boolean onMenuItemSelected(int featureId, Item item) {
	// switch (item.getId()) {
	// case 0:
	// startSubActivity(new Intent(this, DirNameInputActivity.class),
	// SUB_ACTIVITY_REQUEST_CODE);
	// return true;
	// }
	// return false;
	// }
	//
	// // ===========================================================
	// // Reacting on started subactivities that return
	// // ===========================================================
	// @Override
	// protected void onActivityResult(int requestCode, int resultCode,
	// String data, Bundle extras) {
	// super.onActivityResult(requestCode, resultCode, data, extras);
	// // Here We identify the subActivity we starte
	// if(requestCode == SUB_ACTIVITY_REQUEST_CODE){
	// // And show its result
	// try{
	// if(data.length() > 0){
	// String dirName = this.currentDirectory.getPath() + "/" + data + "/";
	// dirName = dirName.replace("//", "/");
	// File f = new File(dirName);
	// boolean success = f.mkdir();
	// if(!success)
	// showAlert("Error", "Could not create the directory: " + data + "\nin: " +
	// this.currentDirectory.getPath(), "Ok",false);
	// }
	// }catch (Exception e){
	// showAlert("Error", "Could not create the directory: " + data + "\nin: " +
	// this.currentDirectory.getPath(), "Ok",false);
	// }
	// }
	// }

	/**
	 * This function browses to the root-directory of the file-system.
	 */
	private void browseToRoot() {
		browseTo(new File("/"));
	}

	/**
	 * This function browses up one level according to the field:
	 * currentDirectory
	 */
	private void upOneLevel() {
		if (this.currentDirectory.getParent() != null)
			this.browseTo(this.currentDirectory.getParentFile());
	}

	private void browseTo(final File aDirectory) {
		// On relative we display the full path in the title.
		if (this.displayMode == DISPLAYMODE.RELATIVE)
			this.setTitle(aDirectory.getAbsolutePath() + " :: "
					+ getString(R.string.app_name));
		if (aDirectory.isDirectory()) {
			this.currentDirectory = aDirectory;
			fill(aDirectory.listFiles());
		} else {
			OnClickListener okButtonListener = new OnClickListener() {
				// @Override
				public void onClick(DialogInterface arg0, int arg1) {
					// Lets start an intent to View the file, that was
					// clicked...
					AndroidFileBrowser.this.openFile(aDirectory);
				}
			};
			OnClickListener cancelButtonListener = new OnClickListener() {
				// @Override
				public void onClick(DialogInterface arg0, int arg1) {
					// Do nothing ^^
				}
			};

			AlertDialog alertDialog = new AlertDialog.Builder(this)
					.setTitle("Question")
					.setMessage(
							"Do you want to open that file?\n"
									+ aDirectory.getName())
					.setPositiveButton("确定", okButtonListener)
					.setNegativeButton("取消", cancelButtonListener).create();
			alertDialog.show();

		}
	}

	private void openFile(File aFile) {

		/*Intent myIntent = new Intent(android.content.Intent.ACTION_VIEW,
				new Uri.parse("file://" + aFile.getAbsolutePath()));
		startActivity(myIntent);*/
		Toast.makeText(this, aFile.getName(), 1000);

	}

	private void fill(File[] files) {
		this.directoryEntries.clear();

		// Add the "." == "current directory"
		this.directoryEntries.add(new IconifiedText(
				getString(R.string.current_dir), getResources().getDrawable(
						R.drawable.folder)));
		// and the ".." == 'Up one level'
		if (this.currentDirectory.getParent() != null)
			this.directoryEntries.add(new IconifiedText(
					getString(R.string.up_one_level), getResources()
							.getDrawable(R.drawable.uponelevel)));

		Drawable currentIcon = null;
		for (File currentFile : files) {
			if (currentFile.isDirectory()) {
				currentIcon = getResources().getDrawable(R.drawable.folder);
			} else {
				String fileName = currentFile.getName();
				/* Determine the Icon to be used, 
				 * depending on the FileEndings defined in:
				 * res/values/fileendings.xml. */
				if (checkEndsWithInStringArray(fileName, getResources()
						.getStringArray(R.array.fileEndingImage))) {
					currentIcon = getResources().getDrawable(R.drawable.image);
				} else if (checkEndsWithInStringArray(fileName, getResources()
						.getStringArray(R.array.fileEndingWebText))) {
					currentIcon = getResources()
							.getDrawable(R.drawable.webtext);
				} else if (checkEndsWithInStringArray(fileName, getResources()
						.getStringArray(R.array.fileEndingPackage))) {
					currentIcon = getResources().getDrawable(R.drawable.packed);
				} else if (checkEndsWithInStringArray(fileName, getResources()
						.getStringArray(R.array.fileEndingAudio))) {
					currentIcon = getResources().getDrawable(R.drawable.audio);
				} else {
					currentIcon = getResources().getDrawable(R.drawable.text);
				}
			}
			switch (this.displayMode) {
			case ABSOLUTE:
				/* On absolute Mode, we show the full path */
				this.directoryEntries.add(new IconifiedText(currentFile
						.getPath(), currentIcon));
				break;
			case RELATIVE:
				/* On relative Mode, we have to cut the
				 * current-path at the beginning */
				int currentPathStringLenght = this.currentDirectory
						.getAbsolutePath().length();
				this.directoryEntries.add(new IconifiedText(currentFile
						.getAbsolutePath().substring(currentPathStringLenght),
						currentIcon));

				break;
			}
		}
		Collections.sort(this.directoryEntries);

		IconifiedTextListAdapter itla = new IconifiedTextListAdapter(this);
		itla.setListItems(this.directoryEntries);
		this.setListAdapter(itla);
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		super.onListItemClick(l, v, position, id);
		int selectionRowID = position;// (int) this.getSelectedItemPosition();
		String selectedFileString = this.directoryEntries.get(selectionRowID)
				.getText();
		if (selectedFileString.equals(getString(R.string.current_dir))) {
			// Refresh
			this.browseTo(this.currentDirectory);
		} else if (selectedFileString.equals(getString(R.string.up_one_level))) {
			this.upOneLevel();
		} else {
			File clickedFile = null;
			switch (this.displayMode) {
			case RELATIVE:
				clickedFile = new File(this.currentDirectory.getAbsolutePath()
						+ this.directoryEntries.get(selectionRowID).getText());
				break;
			case ABSOLUTE:
				clickedFile = new File(this.directoryEntries
						.get(selectionRowID).getText());
				break;
			}
			if (clickedFile != null)
				this.browseTo(clickedFile);
		}
	}

	/**
	 * Checks whether checkItsEnd ends with one of the Strings from fileEndings
	 */
	private boolean checkEndsWithInStringArray(String checkItsEnd,
			String[] fileEndings) {
		for (String aEnd : fileEndings) {
			if (checkItsEnd.endsWith(aEnd))
				return true;
		}
		return false;
	}
}