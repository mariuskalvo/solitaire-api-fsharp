DROP DATABASE IF EXISTS solitaire;
CREATE DATABASE solitaire;
\c solitaire;

DROP USER IF EXISTS solitaire;
CREATE USER solitaire WITH PASSWORD 'solitaire';

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

DROP TABLE IF EXISTS game CASCADE;

CREATE TABLE game (
   id					        UUID PRIMARY KEY DEFAULT uuid_generate_v4() NOT NULL,
   state              JSON NOT NULL,
   created_at			    TIMESTAMP WITHOUT TIME ZONE DEFAULT (NOW() AT TIME ZONE 'utc')
);
