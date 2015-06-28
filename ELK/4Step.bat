:: clear log files from logstash log directory
del C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\*.csv

:: clear old elasticsearch index
node CleanOldIndexes.js

:: copy logs to logstash log directory
xcopy /Y C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\*.csv C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\

:: start logstash
start LogstashTestLognxVideoCsv.bat
