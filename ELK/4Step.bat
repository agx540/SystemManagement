:: clear log files from logstash log directory
del D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\*.*

:: clear old elasticsearch index
node CleanOldIndexes.js

:: copy logs to logstash log directory
xcopy /Y D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\* D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\

:: start logstash
start LogstashTestLognxVideoCsv.bat

pause