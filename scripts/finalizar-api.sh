#!/bin/bash
# Para a API GestaoDeUsuarios
# Uso: ./finalizar-api.sh

echo "==> Parando API GestaoDeUsuarios..."
pkill -f "GestaoDeUsuarios.API" && echo "API parada." || echo "Nenhum processo encontrado."
