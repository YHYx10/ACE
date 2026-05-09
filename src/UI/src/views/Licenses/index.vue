

<template>
  <div class="licenses">
    <div class="header">
      <div class="title">
     License
      </div>
      <div class="info">
        <div class="name">
  Name last name
        </div>
        <div class="value long">
          {{nickname}}
        </div>
      </div>
      <div class="info">
        <div class="name">
   The date of registration
        </div>
        <div class="value">
          {{bithday}}
        </div>
      </div>
      <div class="info">
        <div class="name">
Gender
        </div>
        <div class="value">
          {{getSex}}
        </div>
      </div>
    </div>
    <div class="content">
      <div class="menu">
        <nav-item
          v-for="(nav, index) in list"
          :key="index"
          :item="nav"
          :currentNav="currentNav"
          :setCurrentNav="setCurrentNav"
        />
      </div>
      <div class="wrap">
        <div class="title">
          {{loc('Licenses_' + localizationIndex[list.indexOf(currentNav)])}}
        </div>
        <template v-if="currentCategories.length > 0">
          <div class="item" v-for="(item,key) in currentCategories" :key="key">
            <div class="category">
              <div class="name">Category</div>
              <div class="value">{{loc(item.name)}}</div>
            </div>
            <div v-if="item.date" class="category">
              <div class="name">Receive</div>
              <div class="value">{{item.date}}</div>
            </div>
            <div v-else>Absent</div>
          </div>
        </template>
        <div v-else>Absent</div>
      </div>
    </div>
    <div class="exit" @click="closeLicenses">
      <div class="btn">Esc</div>
      <div class="about">Close</div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'

import NavItem from './components/NavItem'

export default {
  name: 'Licenses',

  components: {
    NavItem,
  },

  data: function() {
    return{
      list: [
        'vehicle',
        'weapon',
        'medical',
        'job',
        'military'
      ],
      localizationIndex: [
        15,
        19,
        9,
        5,
        14
      ],
      currentNav: '',
    }
  },

  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('licenses', [
      'licenses',
      'nickname',
      'gender',
      'bithday',
    ]),
    ...mapGetters('localization',['loc']),
    getSex: function() {
      if (this.gender) {
        return 'M'
      } else {
        return 'F'
      }
    },
    currentCategories: function() {
      let array = this.licenses
      return array.filter(element => element.type === this.currentNav)
    }
  },
  
  methods:{
    closeLicenses: function() {
      window.mp.trigger('Licenses:closeLicenses')
    },
    setCurrentNav: function(value) {
      this.currentNav = value
    }
  },

  mounted() {
    this.currentNav = this.list[0]
  }
}
</script>

<style lang="scss" scoped>
.licenses {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  
  .header {
    width: 27.85rem;
    height: 2.95rem;
    background: rgba(0, 0, 0, 0.4);
    color: #fff;
    gap: 1.5rem;
    font-family: 'Akrobat';
    font-weight: 700;
    text-transform: uppercase;
    display: flex;
    align-items: center;
    justify-content: center;
    .title {
      font-style: normal;
      font-weight: 700;
      font-size: 1.6rem;
    }
    .info {
      font-style: normal;
      .name {
        font-size: 0.5rem;
        line-height: 0.6rem;
      }
      .value {
        font-size: 1rem;
        line-height: 1.2rem;
      }
      .long {
        width: 9.8rem
      }
    }
  }
  .content {
    width: 29.55rem;
    height: 17.3rem;
    background-image: url('/img/licenses/bg.png');
    background-size: cover;
    border-radius: 0.5rem;
    position: relative;
    .menu {
      display: flex;
      gap: 0.177rem;
      flex-direction: column;
      top: 50%;
      left: 0;
      position: absolute;
      transform: translate(-100%, -50%);
    }

    .wrap {
      margin: 1.1rem 4.4rem 0 1.9rem;
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
      text-transform: uppercase;
      color: #424153;
      font-weight: 700;
      .title {
        font-size: 0.8rem;
        line-height: 0.95rem;
      }
      .item {
        display: flex;
        justify-content: space-between;
        > div {
          display: flex;
          flex-direction: column;
          font-weight: 700;
          .name {
            font-size: 0.5rem;
            line-height: 0.6rem;
          }
          .value {
            font-size: 1rem;
            line-height: 1.2rem;
          }
        }
      }
    }
  }

  .exit {
    position: absolute;
    bottom: -2rem;
    left: 50%;
    transform: translate(-50%, 100%);
    display: flex;
    gap: 0.5rem;
    align-items: center;
    color: #fff;
    font-weight: 700;
    &:hover .btn {
      background: rgba(0,0,0, 0.5);
    }
    .btn {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 2.5rem;
      height: 2.5rem;
      background: rgba(0,0,0, 0.4);
      border-radius: 0.15rem;
      font-size: 1rem;
      line-height: 1.2rem;
      border: 0.05rem solid rgba(255, 255, 255, 0.07);
      backdrop-filter: blur(0.15rem);
    }
    .about {
      font-size: 0.9rem;
      line-height: 1.1rem;
      letter-spacing: 0.03em;
    }
  }
}
</style>
