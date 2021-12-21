<template>
  <header>
    <b-container 
      class="my-auto title">
      <h1>dtolab.dev</h1>
    </b-container>
    <b-form-select 
      :style="{ height: '100%' }"
      v-on:change="update_classtype" 
      v-model="classtype" 
      size="sm"
      class="mx-1 select"
    >
      <b-form-select-option :value="'class'">class</b-form-select-option>
      <b-form-select-option :value="'struct'">struct</b-form-select-option>
      <b-form-select-option :value="'record'">record</b-form-select-option>
      <b-form-select-option :value="'record_struct'">record struct</b-form-select-option>
    </b-form-select>
    <b-form-input 
      v-model="namespace" 
      :style="{ width: namespace.length+'em' }" 
      size="sm"
      class="mx-1"
      v-on:keyup="update_namespace" 
      placeholder="ns"></b-form-input>
    <b-form-input 
      v-model="filename" 
      :style="{ width: filename.length+'em' }" 
      size="sm"
      class="mx-1"
      v-on:keyup="update_name" 
      placeholder="dto"></b-form-input>
    <b-button 
      variant="outline-primary" 
      size="sm" v-on:click="download"
      class="mx-1"
    > Download </b-button>
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
      classtype: 'class',
      filename: "dto",
      namespace: "ns",
      classtypes: [
        { value: 'class', text: 'class' },
        { value: 'struct', text: 'struct' },
        { value: 'record', text: 'record' },
        { value: 'record_struct', text: 'record struct' }
      ]
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
  padding-top: 0.15rem;
  padding-bottom: 0.15rem;
  padding-right: 0.5rem;
}
.title {
  flex: 1 1 auto;
  display: flex;
  align-items: center;
  height: 100%;
}
h1 {
  margin: 0;
  vertical-align: middle;
}
.select {
  flex: 0 1 auto;
  width: 15%;
}
button {
  flex: 0 1 auto;
  background-color: var(--header-color);
  color: var(--header-text-color);
}
input {
  flex: 0 1 auto;
  min-width: 15%;
  text-align: center;
}
</style>
