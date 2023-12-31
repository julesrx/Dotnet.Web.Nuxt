import { join } from 'path';
import { existsSync, copyFileSync, writeFileSync, readFileSync } from 'fs';
import { spawnSync } from 'child_process';

const baseFolder = process.env.APPDATA
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`;

const nameFilePath = '.cert.name';
if (!existsSync(nameFilePath)) writeFileSync(nameFilePath, crypto.randomUUID());
const certificateName = readFileSync(nameFilePath);

const certFilePath = join(baseFolder, `.${certificateName}.pem`);
const keyFilePath = join(baseFolder, `.${certificateName}.key`);

if (!existsSync(certFilePath) || !existsSync(keyFilePath)) {
    spawnSync('dotnet', ['dev-certs', 'https', '--trust'], {
        stdio: 'inherit',
    });

    const res = spawnSync(
        'dotnet',
        [
            'dev-certs',
            'https',
            '--export-path',
            certFilePath,
            '--format',
            'Pem',
            '--no-password',
        ],
        { stdio: 'inherit' }
    );

    if (res.status !== 0) process.exit(res.status);
}

copyFileSync(certFilePath, './.cert.pem');
copyFileSync(keyFilePath, './.cert.key');
