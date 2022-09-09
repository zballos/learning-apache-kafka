# Hands-On

Note: Run the commands in PowerShell

## Create a topic

``` powershell
./kafka/bin/windows/kafka-topics.bat --create --topic cursos --bootstrap-server localhost:9094
```

## List topics

``` powershell
.\bin\windows\kafka-topics.bat --list --bootstrap-server localhost:9094,localhost:9095
```

## delete topics

``` powershell
.\bin\windows\kafka-topics.bat --delete --topic cursos --bootstrap-server localhost:9094
```

## Create topic with partition and replication factor

Partitions improves performance with increased read and write capability.

Replication factor brings resiliency and availability to the topic.

``` powershell
.\bin\windows\kafka-topics.bat --create --topic chat --bootstrap-server localhost:9094 --partitions 2 --replication-factor 2
```

## Change Number of Partitions

Increasing the amount of partition increases the topic's read and write capability.

``` powershell
.\bin\windows\kafka-topics.bat --alter --topic chat --bootstrap-server localhost:9094 --partitions 3
```
