# Data Model: SmugglerWeb Core

## Entities

### User
- id (GUID)
- displayName (string, nullable)
- email (string, unique, nullable)
- createdAt (timestamp)
- updatedAt (timestamp)

### ExternalAccount
- id (GUID)
- userId (GUID, FK -> User)
- provider (string)  # e.g., "oidc"
- providerUserId (string)
- createdAt (timestamp)

### Item
- id (GUID)
- name (string, required, max 120)
- description (string, max 1024)
- createdAt (timestamp)
- updatedAt (timestamp)

## Relationships
- User 1 - N ExternalAccount
- User 1 - N Item (옵션)

## Validation Rules
- Item.name: required, length ≤ 120
- Item.description: length ≤ 1024
- ExternalAccount.provider: in { "oidc" }
- Email: RFC 형식(있다면)

## State Transitions
- ExternalAccount: linked -> unlinked (언링크 기능 추가 시)

