import paths from './proxy-paths.json'

const serverUrl = 'http://localhost:7100'

const proxys: { [p: string]: { target: string, changeOrigin: true, secure: false } } = {}
for (const path of paths) {
    proxys[path] = {target: serverUrl + path, changeOrigin: true, secure: false}
}

export default defineNuxtConfig({
    devtools: {enabled: true},
    devServer: {host: 'localhost'},
    nitro: {devProxy: proxys},
    ssr:false
})
