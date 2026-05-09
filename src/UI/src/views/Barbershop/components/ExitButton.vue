<template>
  <div class="exit" @click="exit">
    <span>Esc</span>  - Schließen
  </div>
</template>

<script>
export default {
  name: 'ButtonExit',

  methods: {
    exit: function() {
      window.mp.trigger("closeBarber");
    }
  }
}
</script>

<style lang="scss" scoped>
.exit {
  width: 100%;
  height: 2.22vh;
  font-family: 'Montserrat';
  font-style: normal;
  font-weight: 400;
  font-size: 1.85vh;
  line-height: 2.22vh;
  text-align: center;
  color: #FFFFFF;
  span {
    font-weight: 700;
  }
}
</style>
