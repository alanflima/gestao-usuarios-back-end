#!/bin/bash
# Para o SQL Server e o Colima
# Uso: ./parar-infra.sh

echo "==> Parando container SQL Server..."
docker stop sql_server_dev

echo ""
echo "==> Parando Colima..."
colima stop

echo ""
echo "Infraestrutura parada."
