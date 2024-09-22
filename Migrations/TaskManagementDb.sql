create table users
(
	id uuid primary key,
    username varchar(255) unique not null,
    email varchar(255) unique not null,
    passwordhash varchar(255) not null,
    createdat date
);

create table categories (
    id uuid primary key,
    name varchar(255) unique not null,
    createdAt date
);

create table tasks (
    id uuid primary key,
    title varchar(255) not null,
    description varchar(255),
    iscompleted boolean,
    duedate date,
    userid uuid references users(id) on delete set null,
    categoryid uuid references categories(id) on delete set null,
    priority int check (priority in (1, 2, 3)),
    createdat date
);

create table comments
(
	id uuid primary key,
	taskid uuid references tasks(id) on delete set null,
	userid uuid references users(id) on delete set null,
	content varchar(255) not null,
	createdat date
);

create table taskattachments
(
	id uuid primary key,
	taskid uuid references tasks(id) on delete set null,
	filepath varchar(100) not null,
	createdat date 
);

create table taskhistory
(
	id uuid primary key,
	taskid uuid references tasks(id) on delete set null,
	changedescription varchar(250) not null,
	changeat date 
);



