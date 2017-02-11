Get-ChildItem "D:\ELK\TestLogs\NAS201_eurotech_blau\streams_logs\_Logs\copy" -Filter *.csv | 
Foreach-Object {
    #$(Get-Item $_.FullName).lastwritetime=$(Get-Date "01.01.2015 13:00")
    $(Get-Item $_.FullName).lastwritetime=$(Get-Date)
}