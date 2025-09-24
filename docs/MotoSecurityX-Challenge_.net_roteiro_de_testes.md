# 🗒️ Roteiro de Testes — API MotoSecurityX-Challenge_.net

----
----

## 1. Usuários (CRUD completo + paginação)

### 1.1 Criar Usuário (POST /api/usuarios) 

- Abra POST /api/usuarios

- Clique Try it out

**Corpo de exemplo:**

```json
{
  "nome": "Admin",
  "email": "admin@mottu.com"
}
```
- Clique Execute

**Resultados Esperados:**

- 201 Created ou 200 OK

- Se vier 500 e a mensagem mencionar UNIQUE constraint: você tentou criar um e-mail repetido; troque o e-mail e tente novamente

### 1.2 Listar Usuários (GET /api/usuarios)

- Abra GET /api/usuarios

- Clique Try it out

- Query params: page=1, pageSize=5

- Execute

**Resultados Esperados:**

- 200 OK com um objeto PagedResult<UsuarioDto>:

    items: lista de usuários

    page, pageSize, totalCount

    links: self, prev (se page>1), next (se tiver mais páginas)

- Clique na URL de links.next.href (se houver) para ver a próxima página

### 1.3 Obter por Id (GET /api/usuarios/{id}) 

- Clique Try it out

- Copie um id retornado no passo 1.2

- Abra GET /api/usuarios/{id}, cole o id

- Execute

**Resultados Esperados:**

- 200 OK com o usuário

- 404 Not Found: se o id não existir

### 1.4 Atualizar (PUT /api/usuarios/{id})

- Abra PUT /api/usuarios/{id}

- Clique Try it out

- Use o mesmo id

**Corpo de exemplo:**

```json
{
  "nome": "Admin Atualizado",
  "email": "admin2@mottu.com"
}
```
- Clique Execute

**Resultados Esperados:**

- 204 No Content

- (Opcional) volte no GET {id} para ver o dado atualizado

### 1.5 (DELETE /api/usuarios/{id}) 

- Abra DELETE /api/usuarios/{id}

- Clique Try it out

- Execute com o id do usuário

**Resultados Esperados:**

- 204 No Content

- (Opcional) tente o GET {id} em seguida → deve dar 404 Not Found

----
----

## 2. Pátios (CRUD + preparação para mover moto)

### 2.1 Criar Pátio (POST /api/patios)

- Abra POST /api/patios

- Clique Try it out

**Corpo de exemplo:**

```json
{
  "nome": "Pátio Central",
  "endereco": "Rua das Entregas, 100"
}
```
- Clique Execute

**Resultados Esperados:**

- 201 Created

- Guarde o id do pátio para usar ao mover a moto

### 2.2 Listar Pátios (GET /api/patios?page=1&pageSize=5)

- Abra GET /api/patios

- Clique Try it out

- Execute

**Resultados Esperados:**

- 200 OK com PagedResult<PatioDto>

- Observe o campo quantidadeMotos (quando começar a mover motos, isso deve aumentar).

### 2.3 Obter por Id (GET /api/patios/{id}) 

- Clique Try it out

- Copie um id retornado no passo 2.2

- Abra GET /api/patios/{id}, cole o id

- Execute

**Resultados Esperados:**

- 200 OK com o pátio

- 404 Not Found: se o id não existir

### 2.4 Atualizar (PUT /api/patios/{id})

- Abra PUT /api/patios/{id}

- Clique Try it out

- Use o mesmo id

**Corpo de exemplo:**

```json
{
  "nome": "Pátio Mooca",
  "endereco": "Rua do Oratório, 788"
}
```
- Clique Execute

**Resultados Esperados:**

- 204 No Content

- (Opcional) volte no GET {id} para ver o dado atualizado

### 2.5 (DELETE /api/patios/{id}) 

- Abra DELETE /api/patios/{id}

- Clique Try it out

- Use o id do pátio

- Execute com o id

----
----

## 3. Motos (CRUD + mover para pátio)

### 3.1 Criar Moto (POST /api/motos)

- Abra POST /api/motos

- Clique Try it out

**Corpo de exemplo:**

```json
{
  "placa": "abc1d23",
  "modelo": "Mottu 110i"
}
```
- Clique Execute

**Resultados Esperados:**

- 201 Created com Location e/ou corpo do recurso.

**Dica:** 

- A placa é normalizada (ABC1D23). Guarde o id da moto.

- Se aparecer erro sobre placa: confirme que tem 7–8 caracteres e só letras/dígitos. Hífen e espaços são removidos automaticamente. 

### 3.2 Listar Motos (GET /api/motos?page=1&pageSize=5)

- Abra GET /api/motos

- Clique Try it out

- Execute

**Resultados Esperados:**

- 200 OK com PagedResult<MotoDto>

- Veja links.self, links.next (se houver), etc.

### 3.3 Obter Moto por Id (GET /api/motos/{id})

- Abra GET /api/motos/{id}

- Clique Try it out

- Use o id recém-criado.

- Execute

**Resultados Esperados:**

- 200 OK com dentroDoPatio=false e patioId=null

### 3.4 Mover Moto para o Pátio (POST /api/motos/{id}/mover)

- Abra POST /api/motos/{id}/mover

- Clique Try it out

- Use o id da moto e o id do pátio criado no passo 2.1

**Corpo de exemplo:**

```json
{
  "patioId": "PASTE_AQUI_O_GUID_DO_PATIO"
}
```
- Clique Execute

**Resultados Esperados:**

- 204 No Content

### 3.5 Verificar Moto após mover (GET /api/motos/{id})

- Abra GET /api/motos/{id}

- Clique Try it out

- Execute novamente o GET /api/motos 

- Use o id da moto

- Execute

**Resultados Esperados:**

- 200

- dentroDoPatio=true e patioId = GUID do pátio.

### 3.6 Atualizar Moto (PUT /api/motos/{id})

- Abra PUT /api/motos/{id}

- Clique Try it out

- Use o id da moto 

**Corpo de exemplo:**

```json
{
  "modelo": "Mottu 125i",
  "placa": "XYZ9A88"
}
```
- Clique Execute

**Resultados Esperados:**

- 204 No Content

- Execute GET /api/motos novamente para conferir mudança

### 3.7 Deletar Moto (DELETE /api/motos/{id})

- Abra DELETE /api/motos/{id}

- Clique Try it out

- Use o id da moto

- Execute

**Resultados Esperados:**

- 204 No Content

- Execute GET /api/motos novamente para conferir mudança

## 4. (Opcional) Resetar DB para recomeçar os testes

- Pare a API

- Apague o arquivo CP4.MotoSecurityX.Api\motosecurityx.db

**Rode:**

dotnet ef database update -p .\CP4.MotoSecurityX.Infrastructure\ -s .\CP4.MotoSecurityX.Api\
dotnet run --project .\CP4.MotoSecurityX.Api\ --launch-profile https

