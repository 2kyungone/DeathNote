package com.goat.deathnote.domain.music.controller;

import com.goat.deathnote.domain.music.entity.Music;
import com.goat.deathnote.domain.music.service.MusicService;
import com.goat.deathnote.domain.soul.entity.Soul;
import com.goat.deathnote.domain.soul.service.SoulService;
import com.goat.deathnote.domain.world.entity.World;
import com.goat.deathnote.domain.world.service.WorldService;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@RequiredArgsConstructor
@RequestMapping("/musics")
public class MusicController {

    private final MusicService musicService;

    @PostMapping
    public Music createMusic(@RequestBody Music music) {
        return musicService.saveMusic(music);
    }

    @GetMapping
    public ResponseEntity<List<Music>> getAllMusics() {
        return ResponseEntity.ok(musicService.getAllMusics());
    }

    @GetMapping("/{id}")
    public ResponseEntity<Optional<Music>> getMusicById(@PathVariable Long id) {
        return ResponseEntity.ok(musicService.getMusicById(id));
    }

}