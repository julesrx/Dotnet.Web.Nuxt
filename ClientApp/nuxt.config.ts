export default defineNuxtConfig({
    devtools: {enabled: true},
    devServer: {host: 'localhost'},
    nitro: {
        devProxy: {
            '/_': {
                target: 'https://localhost:7100/_',
                changeOrigin: true,
                secure: false
            }
        }
    },
    ssr: false
})
