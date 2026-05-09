<template>
  <div class="quests-tab">
    <div class="coming-soon"></div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'QuestsTab',
  computed: {
    ...mapGetters('localization', ['loc'])
  }
}
</script>

<style lang="scss" scoped>
.quests-tab{
  width: 100%;
  height: 40rem;
  position: relative;
  .coming-soon{
    width: 21.9rem;
    height: 16.2rem;
    background: url('/img/optionsMenu/programTab/coming-soon.png') center / cover no-repeat;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }
}
</style>
