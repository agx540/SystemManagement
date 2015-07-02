:: clear log files from logstash log directory
del C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\nxVideoProvider\copy\*.log

:: clear old elasticsearch index
node CleanOldIndexesWard.js

:: copy logs to logstash log directory
xcopy /Y C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\nxVideoProvider\*.log C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\nxVideoProvider\copy\

:: start logstash
start LogstashTestLogWardFile.bat
