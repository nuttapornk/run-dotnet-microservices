
## Add migration
from root directory
```
dotnet ef migrations add "initialDatabase" --project .\src\Services\Ordering\Ordering.Infrastructure\ --startup-project .\src\Services\Ordering\Ordering.Api\ --output-dir Persistence\Migrations
```