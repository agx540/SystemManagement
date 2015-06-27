:: clear old elasticsearch index
node CleanOldIndexes.js


:: copy logs to logstash log directory
xcopy C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\EuroTech201Statistics_20150530-000000.csv C:\Users\Alexander\Desktop\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy\EuroTech201Statistics_20150607-000000.csv
