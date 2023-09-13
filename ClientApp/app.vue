<script lang="ts" setup>
// const {data: message} = await useFetch<string>('/_/hello')

const {isAuthenticated, login} = useAuth()
</script>

<template>
  {{ isAuthenticated }}
  <button @click="login">Login</button>
  <!--  <p>The server wants to say hello: <b>{{ message }}</b></p>-->
</template>
