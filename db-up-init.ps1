# docker compose up
docker-compose up -d

pushd ./CartService
dotnet ef database update
popd

pushd ./CatalogService
dotnet ef database update
popd

pushd ./CustomerService
dotnet ef database update
popd

pushd ./OrderService
dotnet ef database update
popd

pushd ./StockService
dotnet ef database update
popd