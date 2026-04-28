#!/bin/bash
# Inicia o Colima e o SQL Server
# Uso: ./iniciar-infra.sh

export PATH="/usr/local/bin:/opt/homebrew/bin:$PATH"

echo "==> Iniciando Colima..."
colima start --cpu 2 --memory 4 --disk 20

echo ""
echo "==> Iniciando container SQL Server..."
docker start sql_server_dev

echo ""
echo "Infraestrutura pronta:"
echo "  SQL Server : localhost:1433 (sa / Antares9\$)"
echo "  Database   : DB1677_ADA_GestaoDeUsuarios"
echo ""
docker ps --filter name=sql_server_dev --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"
