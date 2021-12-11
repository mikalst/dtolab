<template>
  <header>
    <h1>dtolab.dev</h1>
    <select v-on:change="update_classtype" v-model="classtype" id="cars" name="cars">
      <option value="class" selected>Class</option>
      <option value="struct">Struct</option>
      <option value="record">Record</option>
      <option value="record_struct">Record Struct</option>
    </select>
    <input v-model="namespace" v-on:keyup="update_namespace" placeholder="ns"> 
    <input v-model="filename" v-on:keyup="update_name" placeholder="dto"> 
    <button v-on:click="download"> Download </button>
  </header>
</template>

<script>
export default {
  name: 'Banner',
  methods: {
    download: function() {
      this.$emit("download");
    },
    update_namespace: function() {
      this.$emit("namespace_changed", this.namespace ? this.namespace : "ns");
    },
    update_name: function() {
      this.$emit("name_changed", this.filename ? this.filename : "dto");
    },
    update_classtype: function() {
      this.$emit("classtype_changed", this.classtype);
    }
  },
  data: function() {
    return {
      classtype: "class",
      filename: "dto",
      namespace: "ns"
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
header {
  text-align: left;
  display: flex;
  color: var(--header-text-color);
  background-color: var(--header-color);
  padding-top: 0.5rem;
  padding-bottom: 0.5rem;
  padding-right: 0.5rem;
}
h1 {
  flex: 1 1 auto;
}
button {
  flex: 0 1 auto;
  align-content: left;
  width: 15%;
  text-align: center;
  background-color: var(--header-color);
  border: 2px solid var(--header-text-color);
  color: var(--header-text-color);
}
input {
  flex: 0 1 auto;
  align-content: right;
  width: 15%;
  text-align: center;
  border: 2px solid var(--header-text-color);
  color: var(--header-text-color);
}
</style>
