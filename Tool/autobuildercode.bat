protoc.exe -I=.   --csharp_out=. --grpc_out=. --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe protocBuilder/configDcUtil.proto
pause