# Dotnet.Web.Nuxt

Custom .NET 6+ template using [Nuxt 3](https://nuxt.com/) as frontend and utilizing [ASP.NET Core's SPA](https://learn.microsoft.com/en-us/aspnet/core/client-side/spa/intro) feature during development.

## Usage

This template can be installed in multiple ways.

### Using `dotnet new`

1. clone the repository
2. in the repository, run `dotnet install .`
3. once installed, run `dotnet new nuxt -o <project-directory>` to create a project using the template

### Using GitHub's template feature

1. create a repository using this template ([see the official documentation](https://docs.github.com/en/repositories/creating-and-managing-repositories/creating-a-repository-from-a-template))
2. remove the `.template.config` directory
3. rename the `csproj` with the project you want
4. replace the namespaces in the source files

### Downloading the full repo zip file

1. downloading the full repo zip file
2. extract to the disired location
3. repeat the [template](#using-githubs-template-feature) steps 2 through 4

## How it works

During development, .NET automatically starts the Nuxt development server.  
The Nuxt server is then configured to proxy all of the .NET server's endpoints, so that the front end can communicate to the backend without having to change the URL between development and production environments.  
This mapping is generated using the [`ProxyGenerator.cs`](ProxyGenerator.cs) file, which is automatically removed in production.

For example, if the backend app maps a new endpoint :

```cs
app.MapGet("/products", () => [...]);
```

Then, a new entry is added to the `proxy-paths.json` file :

```json
[
  // ...
  "/products"
]
```

This file is then imported by the [`nuxt.config.ts`](nuxt.config.ts) file to configure the dev proxy.

See the [nitro documentation](https://nitro.unjs.io/config#devproxy) for more details on how the proxy mapping works.
