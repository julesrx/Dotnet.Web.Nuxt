# Dotnet.Web.Nuxt

Custom .NET 6+ template using [Nuxt 3](https://nuxt.com/) as front-end and utilizing [ASP.NET Core's SPA](https://learn.microsoft.com/en-us/aspnet/core/client-side/spa/intro) feature during development.

## Usage

This template can be installed in multiple ways :

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
2. extract to the desired location
3. repeat the [template](#using-githubs-template-feature) steps 2 through 4

## How it works

During development, .NET automatically installs the dependencies with [`pnpm`](https://pnpm.io/) and starts the Nuxt development server with `pnpm dev`.  
This will also generate the missing https certificates with the [`dotnet dev-certs`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs) command.

The Nuxt dev server is then configured to proxy the `/_` endpoint to the .NET server.  
This allows the front-end to communicate to the back-end since they run on different ports.

See the [nitro documentation](https://nitro.unjs.io/config#devproxy) for more details on how the proxy mapping works.
