@echo off 
echo 正在清除无用文件，请稍等一会...... 

del *.pdb
del *.xml  
del/s/q log\*.* 
rd log
del DockPanel.config
del *.manifest
del *.vshost.exe*

echo 清理完成! 
:echo. & pause