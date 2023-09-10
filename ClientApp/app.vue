<script lang="ts" setup>
const { data: message } = await useFetch<string>('/data')
</script>

<template>
 <p>{{ message}}</p>
</template>
