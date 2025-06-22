# 启动步骤
- 执行`docker up -d`命令拉取镜像
- 调整`Fly.CleanArchitecture.Sample.Api`项目的`appsettings.json`文件
- `Fly.CleanArchitecture.Sample.Api`项目下执行命令`dotnet ef database update -p ..\Fly.CleanArchitecture.Sample.Infrastructure\ `迁移数据库