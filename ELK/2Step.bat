:: clear log files from logstash log directory
del D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\EuroTech201Statistics_20150607-000000.csv


:: clear old elasticsearch index
.\node CleanOldIndexes.js


:: copy logs to logstash log directory
xcopy D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\EuroTech201Statistics_20150530-000000.csv D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\EuroTech201Statistics_20150607-000000.csv
