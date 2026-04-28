#!/bin/bash
# Inicia a API GestaoDeUsuarios com o perfil LocalMacMock
# Uso: ./iniciar-api.sh

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT="$SCRIPT_DIR/../src/GestaoDeUsuarios.API/GestaoDeUsuarios.API.csproj"

echo "==> Iniciando API GestaoDeUsuarios..."
echo "  URL : http://localhost:5120"
echo "  Env : LocalMacMock"
echo ""
ASPNETCORE_ENVIRONMENT=LocalMacMock ASPNETCORE_URLS="http://localhost:5120" dotnet run --project "$PROJECT" --no-launch-profile
