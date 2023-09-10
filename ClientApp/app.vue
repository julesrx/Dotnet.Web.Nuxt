<script lang="ts" setup>
const { data: message } = await useFetch<string>('/hello')
</script>

<template>
 <p>The server wants to say hello: <b>{{ message}}</b></p>
</template>
