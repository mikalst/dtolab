<template>
  <section>
  <DisplayEntity 
    v-for="value in files" :key="value" 
    :file="value"/>
  </section>
</template>

<script>
import DisplayEntity from './DisplayEntity.vue'

export default {
  name: 'DisplayArea',
  components: {
    DisplayEntity
  },
  props: {
    files: {}
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
section {
  line-height: 1.0;
}
</style>
