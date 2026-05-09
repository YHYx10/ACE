<template>
  <div class="title">
    <div class="name">
      {{ titlePrimary }} <span> {{ titleSecondary }}</span>
    </div>
    <div class="about">
      {{ about }}
    </div>
  </div>
</template>

<script>
export default {
  name: 'TitleComponent',
  props: {
    titlePrimary: String,
    titleSecondary: String,
    about: String,
  },
}
</script>

<style lang="scss" scoped>
.title {
  font-family: 'Akrobat';
  .name {
    font-weight: 800;
    text-transform: uppercase;
    font-size: 3.70vh;
    line-height: 4.44vh;
    color: #ffffff;
    & span {
      text-transform: none;
      background: linear-gradient(
        180deg,
        #301934  0%,
        #ea0505 49.48%,
        #301934  100%
      );
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-shadow: 0vw 0vw  4.53vh rgba(255, 0, 0, 0.52);
    }
  }

  .about {
    margin-top: 0.463vh;
    font-weight: 500;
    font-size: 1.66vh;
    line-height: 2.037vh;
    color: #ffffff;
  }
}
</style>
