<script lang="ts" setup>
const { data: message } = await useFetch<string>('/_/hello')
await useFetch<string>('/_/auth/token',{method:'POST'})
</script>

<template>
 <p>The server wants to say hello: <b>{{ message }}</b></p>
</template>
