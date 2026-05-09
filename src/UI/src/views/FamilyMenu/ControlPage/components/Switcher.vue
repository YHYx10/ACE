<template>
  <label class="switch">
    <input :value="value" :checked="value" @input="$emit('input', !value)" type="checkbox" />
    <div></div>
  </label>
</template>

<script>
export default {
  props: {
    value: Boolean
  }
}
</script>

<style lang="scss" scoped>
.switch {
  display: inline-block;
  font-size: 1.852vh;
  height: 2.407vh;
  width: 4.167vh;
  background: rgba(255, 255, 255, 0.05);
  border-radius: 3.426vh;
  padding: 0.37vh 0.463vh;
  input {
    position: absolute;
    opacity: 0;
  }
  div {
    height: 1.667vh;
    width: 1.667vh;
    border-radius: 100%;
    background: #301934 ;
    -webkit-transition: all 300ms;
    -moz-transition: all 300ms;
    transition: all 300ms;
  }
}

.switch input:checked + div {
  -webkit-transform: translate3d(100%, 0, 0);
  -moz-transform: translate3d(100%, 0, 0);
  transform: translate3d(100%, 0, 0);
  background: #5cff80;
}
</style>
