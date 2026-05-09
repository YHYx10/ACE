<template>
  <div class="frac-memb">
    <div class="frac-memb-heading">
      <div class="frac-memb-amount">
        <div class="category">The number of participants</div>
        <div class="title">{{ fracMembers.length }}</div>
      </div>
      <div class="frac-memb-invite">
        <button class="frac-memb-invite-btn" @click="invitePlayer">+</button>
        <div class="frac-memb-invite-text">
          <div class="category">invite</div>
          <div class="title"> New participant</div>
        </div>
      </div>
      <div class="frac-memb-capt" @click="openCaptTeam">
        {{ loc('optmenu:info:ter') }}
      </div>
    </div>
    <div class="frac-memb-list-head">
      <div class="frac-memb-list-search">
        <img src="/img/optionsMenu/organizationTab/search.svg">
        <input :placeholder="loc('mmain:frac:memb:search')" v-model="filter"/>
      </div>
      <div class="frac-memb-list-head-main">
        <div class="frac-memb-list-id" @click="selectSort('id')" :class="{'frac-sort-selected': sortBy == 'id'}">Id
        </div>
        <div class="frac-memb-list-status" @click="selectSort('status')"
             :class="{'frac-sort-selected': sortBy == 'status'}">{{ loc('mmain:frac:memb:status') }}
        </div>
        <div class="frac-memb-list-name" @click="selectSort('name')" :class="{'frac-sort-selected': sortBy == 'name'}">
          {{ loc('mmain:frac:memb:name') }}
        </div>
        <div class="frac-memb-list-rank" @click="selectSort('rank')" :class="{'frac-sort-selected': sortBy == 'rank'}">
          {{ loc('mmain:frac:memb:rank') }}
        </div>
      </div>
    </div>
    <div class="frac-memb-list">
      <div class="frac-memb-list-item" v-for="(member, index) in sortedMembers" :key="index">
        <div class="frac-memb-list-id">
                    <span v-if="member.id > -1">
                        {{ member.id }}
                    </span>
          <span v-else>
                        -
                    </span>
        </div>
        <div class="frac-memb-list-status">
          <div class="frac-memb-list-status-off" v-if="member.id < 0">Offline</div>
          <div class="frac-memb-list-status-on" v-else>Online</div>
        </div>
        <div class="frac-memb-list-name">{{ member.username.replace('_', ' ') }}</div>
        <div class="frac-memb-list-rank">{{ member.rank }}</div>
        <div class="frac-memb-list-setrank item__btn" @click="setRank(member.username, member.rank)">Change the rank</div>
        <div class="frac-memb-list-kick item__btn" @click="kick(member.username)">fire</div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapMutations, mapState} from 'vuex'

export default {
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapGetters('optionsMenu', ['fracMembers']),
    ...mapState('optionsMenu', ['fraction']),
    sortedMembers() {
      let sorted = [...this.fracMembers];
      switch (this.sortBy) {
        case "id":
          sorted.sort((a, b) => {
            if (a.id < 0) return 1;
            if (b.id < 0) return -1;
            if (a.id > b.id) return 1;
            if (a.id < b.id) return -1;
            if (a.id == b.id) return 0;
          });
          break;
        case "status":
          sorted.sort(function (a, b) {
            if (a.id < 0) return 1;
            if (a.id == b.id) return 0;
            return -1;
          });
          break;
        case "rank":
          sorted.sort(function (a, b) {
            return a.rank - b.rank;
          });
          break;
        case "name":
          sorted.sort(function (a, b) {
            if (a.username < b.username) return -1;
            if (a.username > b.username) return 1;
            return 0;
          });
          break;
        default:
          break;
      }
      if (this.reverse) sorted.reverse()
      return this.filter.length > 2 ? sorted.filter(u => u.username.toLowerCase().includes(this.filter.toLowerCase())) : sorted;
    }
  },
  data() {
    return {
      lastCheck: 0,
      sortBy: "name",
      filter: "",
      reverse: false
    }
  },
  methods: {
    openCaptTeam() {
      if (this.lastCheck > Date.now()) return;
      this.lastCheck = Date.now() + 1000;
      window.mp.trigger("cef:mmenu:capt:open")
    },
    selectSort(sort) {
      if (this.sortBy == sort)
        this.reverse = !this.reverse;
      else {
        this.reverse = false
        this.sortBy = sort;
      }
    },
    kick(name) {
      if (this.lastCheck > Date.now()) return;
      this.lastCheck = Date.now() + 1000;
      if (!this.fraction.canKick) {
        window.setData('notifyList/notify', {type: 1, position: 2, message: "mmain:frac:nopermission", time: 3000});
        return;
      }
      this.setDialog({
        input: undefined,
        callback: (val) => {
          window.mp.triggerServer('fmenu:kick', name);
          if (val) return;
        },
        value: '',
        placeholder: '',
        tittle: `mmain:frac:dialog:kick:tit@${name}`,
        subtittle: 'mmain:frac:dialog:kick:sub',
        bg: 'invite'
      });
    },
    setRank(name, rank) {
      if (this.lastCheck > Date.now()) return;
      this.lastCheck = (Date.now() + 1000);
      if (!this.fraction.canRank) {
        window.setData('notifyList/notify', {type: 1, position: 2, message: "mmain:frac:nopermission", time: 3000});
        return;
      }
      this.setDialog({
        input: 'number',
        callback: (val) => {
          if (+val < 0) {
            window.setData('notifyList/notify', {
              type: 1,
              position: 2,
              message: "mmain:frac:dialog:data:wrong",
              time: 3000
            });
          } else {
            window.mp.triggerServer('fmenu:rank', name, +val);
          }
        },
        value: rank,
        placeholder: '',
        tittle: `mmain:frac:dialog:rank:tit@${name}`,
        subtittle: 'mmain:frac:dialog:rank:sub',
        bg: 'invite'
      });
    },
    invitePlayer() {
      if (this.lastCheck > Date.now()) return;
      this.lastCheck = Date.now() + 1000;
      if (!this.fraction.canInvite) {
        window.setData('notifyList/notify', {type: 1, position: 2, message: "mmain:frac:nopermission", time: 3000});
        return;
      }
      this.setDialog({
        input: 'number',
        callback: (val) => {
          window.mp.triggerServer('fmenu:invite', +val);
        },
        value: '',
        placeholder: 'mmain:frac:dialog:invite:pl',
        tittle: `mmain:frac:dialog:invite:tit`,
        subtittle: 'mmain:frac:dialog:rank:sub',
        bg: 'invite'
      });
    },
    ...mapMutations('optionsMenu', ['setDialog'])
  },
}
</script>

<style lang="scss" scoped>
.frac-memb {
  flex: 1 1 100%;

  &-heading {
    display: flex;
    align-items: center;
    margin: 0 0 2rem 0.7rem;
  }

  &-amount {
    margin-right: 4rem;
  }

  &-invite {
    display: flex;
    margin: 0 auto 0 0 ;

    &-btn {
      margin-right: 1rem;
      background: none;
      font-size: 4.5rem;
      line-height: 0;
      color: #fff;
      font-weight: 200;
      filter: drop-shadow(0px 0px 14px rgba(255, 255, 255, 0.55));
    }

    .title {
      color: #5CFF80;
    }

    .category:before {
      display: none;
    }
  }

  &-capt {
    margin-left: 4rem;
    cursor: pointer;
    padding: 1.1rem 2.5rem;
    color: #fff;
    border: 1px solid rgba(255, 255, 255, 0.1);
    background-position: center -0.7px;
    background-image: linear-gradient(0deg, rgba(92, 255, 128, 0.09) 0%, rgba(196, 0, 0, 0) 60%);

    &:hover {
      background-image: linear-gradient(0deg, rgba(92, 255, 128, 0.2) 0%, rgba(196, 0, 0, 0) 100%);
    }
  }

  &-list {
    margin-top: 0.5rem;
    height: 32rem;
    overflow-y: scroll;

    scrollbar-width: thin;
    scrollbar-color: #5cff80 #444444;

    &::-webkit-scrollbar {
      display: block;
      width: 0.1rem;
      height: 0;
    }

    &::-webkit-scrollbar-track {
      background: #444444;
    }

    &::-webkit-scrollbar-thumb {
      background-color: #5cff80;
    }

    &-head-main {
      display: flex;
      color: rgba(255, 255, 255, 0.55);
      text-align: left;
      font-size: 0.8rem;
    }

    &-search {
      width: 100%;
      display: flex;
      align-items: center;
      padding: 0.5rem 1rem;
      margin-bottom: 2rem;
      border: 1px solid;
      border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 50%, rgba(0, 0, 0, 0) 100%) 1;
      background: linear-gradient(90deg, rgba(255, 255, 255, 0) 10%, rgba(255, 255, 255, 0.05) 35%, rgba(12, 16, 10, 0) 70%);

      & input {
        width: 100%;
        background: none;
        color: #fff;
        padding: 0 10rem 0 1rem;

        &::placeholder {
          text-transform: uppercase;
          color: rgba(#fff, .3);;
        }
      }
    }

    &-id {
      width: 6rem;
      margin-right: 2rem;
      text-align: center;
    }

    &-status {
      width: 4rem;

      &-on {
        color: #5CFF80;
      }

      &-off {
        color: rgba(255, 255, 255, 0.3);
      }
    }

    &-name {
      width: 11rem;
      max-height: 2.5rem;
      overflow: hidden scroll;
      margin-left: 4rem;
      &::-webkit-scrollbar {
        display: none;
      }
    }

    &-rank {
      margin: 0 4rem 0 2rem;
      width: 3rem;
    }

    &-kick {
      margin-left: 0.5rem;
    }

    &-item {
      position: relative;
      height: 4rem;
      width: 100%;
      display: flex;
      align-items: center;
      color: #fff;
      margin-top: .5rem;
      border: 1px solid;
      border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 50%, rgba(0, 0, 0, 0) 100%) 1;
      background: linear-gradient(90deg, rgba(255, 255, 255, 0) 10%, rgba(255, 255, 255, 0.05) 35%, rgba(12, 16, 10, 0) 70%);

      &:before {
        content: '';
        position: absolute;
        height: 1.44rem;
        left: 0.9rem;
        color: #fff;
        background: #fff;
        border: 1px solid #fff;
        box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
        transition: 0.2s ease;
      }
      &:hover:before {
        left: 0rem;
        height: 100%;
      }
    }
  }
}

.frac-sort-selected {
  color: #fff;
}
</style>