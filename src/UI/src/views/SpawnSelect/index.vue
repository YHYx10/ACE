<template>
<body>
  <main class="main">
    <div class="select_spawn">
      <div class="select_spawn__container container">
        <h1 class="select_spawn__title">
       Choosing a place for spawn
        </h1>
        <p class="select_spawn__descr">
       A brief description of this interface, why and why.
        </p>

        <div class="select_spawn__items">
          <spawn-item
            v-for="(item, index) in spawns"
            :item="item"
            :key="index"
            :index="index"
          />
        </div>
      </div>
    </div>
  </main>
</body>

</template>

<script>
import { mapState } from 'vuex'
import SpawnItem from './SpawnItem'
export default {
  name: 'SpawnSelect',

  components: {
    SpawnItem
  },

  computed: {
    ...mapState('spawnSelect', ['spawns', 'spawns_keys'])
  }
}
</script>

<style lang="scss" scoped>
body {
  margin: 0;
  font-family: 'Akrobat';
  overflow-anchor: none;
  background-image: url('/img/spawnSelect/bg-home.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  color: #fff;
  height: 100vh;
  width: 100vw;
}


.container {
  max-width: 63.85696040868455vh;
  padding: 0 0 0 5.108556832694764vh;
}

.select_spawn {
  &__title {
    font-weight: 800;
    font-size: 6.130268199233717vh;
    line-height: 7.407407407407407vh;
    background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
    text-fill-color: transparent;
    text-shadow: 0 0 14.303959131545339vh rgba(255, 255, 255, 0.25);
  }

  &__descr {
    font-weight: 600;
    font-size: 1.9157088122605364vh;
    line-height: 2.2988505747126435vh;
    text-transform: uppercase;
    color: #FFFFFF;
    margin-bottom: 2.554278416347382vh;
  }

  &__items {
    display: flex;
    flex-direction: column;
  }
}
</style>
