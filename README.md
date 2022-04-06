# Rhodes Island Data Center

***⚠️ 本项目属于实验性项目，不稳定 ⚠️***

## 构建本机应用

需要使用 `.NET SDK 6.0.x` 版本。

API 项目：

```shell
cd src/RIDC.App.Api
dotnet publish -c Release -r <RID> --no-self-contained
```

Updater 项目

```shell
cd src/RIDC.App.Updater
dotnet publish -c Release -r <RID> --no-self-contained
```

关于 RID，请参考 [微软文档](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)，若想创建自包含的发布版本，替换 `--no-self-contained` 为 `--self-contained` 即可。

## 构建 Docker Image

参考 [MaaDs 的 Dockerfile](https://github.com/MaaAssistantArknights/MaaDownloadServer/blob/main/Dockerfile)

## 运行

你需要准备：

- 非自包含应用，需要 .NET Runtime 6.0.x
- 数据库，可以是 `MySql 5.7.x` `MySql 8.0.x` `MariaDb 10.8.x` `PostgreSQL 14.x` `Sqlite 3.x`，在 `appsettings.json` 中配置数据库类型和数据库连接字符串
- 存储服务器 (可以不使用)，兼容 Amazon S3 API，推荐使用 `Minio` 或 `Amazon S3`，其他的可能也可以使用
- Elasticsearch (可以不使用)，用于日志收集和分析，在 `appsettings.json` 中配置

Updater 和 API 是分开的，但是两者使用同一个配置文件。

若在本机启动，请复制两份一样的配置文件。

若使用 Docker 启动，建议使用 Docker Compose 一起启动两个应用。

## 注意

***⚠️ 本项目属于实验性项目，不稳定 ⚠️***

***⚠️ 本项目属于实验性项目，不稳定 ⚠️***

***⚠️ 本项目属于实验性项目，不稳定 ⚠️***

本项目为实验项目，不稳定，Updater 项目有很多问题，运行时请保证 Updater 项目有 500 MB 的可 Allocate 的内存。

## 项目依赖和包引用

请查看 [Dependency Graph](https://github.com/MaaAssistantArknights/RhodesIslandDataCenter/network/dependencies)

## 许可证

本项目使用 [GNU AFFERO GENERAL PUBLIC LICENSE Version 3](./LICENSE) 授权
