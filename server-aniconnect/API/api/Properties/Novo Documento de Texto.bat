@echo off
sc config wuauserv start= auto
sc config bits start= delayed-auto
sc config cryptsvc start= auto
sc config trustedinstaller start= auto
sc config vds start= demand
sc config vmicvss start= manual
sc config vmicheartbeat start= manual
sc config vmicshutdown start= manual
sc config vmicexchange start= manual
sc config vmickvpexchange start= manual
sc config LxssManager start= auto
sc config vmcompute start= auto
sc config vmms start= auto
sc config hns start= auto
sc config wslservice start= auto
pause
