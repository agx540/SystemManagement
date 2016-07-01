:: clear log files from logstash log directory
del D:\ELK\TestLogs\NAS201_eurotech_blau\nxVideoProvider\copy\*.log

:: clear old elasticsearch index
node .\CleanOldIndexesWard.js

:: copy logs to logstash log directory
xcopy /Y D:\ELK\TestLogs\NAS201_eurotech_blau\nxVideoProvider\KleineDatei.log D:\ELK\TestLogs\NAS201_eurotech_blau\nxVideoProvider\copy\

:: start logstash
start LogstashTestLogWardFile.bat
