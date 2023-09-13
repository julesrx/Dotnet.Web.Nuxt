<script lang="ts" setup>
const {isAuthenticated, login} = useAuth()

const message = ref<string>()
watch(isAuthenticated, async (isAuthenticated) => {
  if (isAuthenticated) {
    message.value = await $authFetch<string>('/_/hello')
  }
}, {immediate: true})
</script>

<template>
  {{ isAuthenticated }}
  <button @click="login">Login</button>
  {{ message }}
  <!--  <p>The server wants to say hello: <b>{{ message }}</b></p>-->
</template>
