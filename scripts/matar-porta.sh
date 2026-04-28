#!/bin/bash
# Mata o processo ocupando uma porta específica
# Uso: ./matar-porta.sh <porta>

PORT=${1:-5120}

PID=$(lsof -ti :$PORT)

if [ -z "$PID" ]; then
  echo "Nenhum processo usando a porta $PORT."
  exit 0
fi

COMMAND=$(ps -p $PID -o command= 2>/dev/null)
echo "==> Encerrando processo na porta $PORT..."
echo "    PID     : $PID"
echo "    Processo: $COMMAND"

kill $PID && echo "Processo encerrado." || echo "Falha ao encerrar o processo."
