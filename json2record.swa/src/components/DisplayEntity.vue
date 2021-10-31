<template>
  <CodeEditor 
    :value="file" 
    :read_only="true"
    :language="'cpp'"
    :languages="languages()"
    :autofocus="true"
    :width="'100%'"
    :border_radius="'0px'"
    ></CodeEditor>
</template>

<script>
import CodeEditor from 'simple-code-editor';

export default {
  name: 'DisplayEntity',
  components: {
    CodeEditor
  },
  props: {
    file: {} 
  },
  methods: {
    languages: function () {
      return [
        ['chsarp', 'c#']
      ];
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
div {
  text-align: left;
}
ul {
  text-align: left;
}

</style>
